using ExpenseTracker.BaseController;
using ExpenseTracker.DbContexts;
using ExpenseTracker.DTO.User;
using ExpenseTracker.Model;
using ExpenseTracker.Models;
using ExpenseTracker.Options;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Services.IdentityServices
{
    public class IdentityServices : IIdentityServices
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JwtSettings _jwtSettings;
        private readonly MyDbContext _dbContext;

        public IdentityServices(
                IWebHostEnvironment webHostEnvironment,
                UserManager<ApplicationUser> userManager,
                JwtSettings jwtSettings,
                MyDbContext dbContext
               )
        {
            _webHostEnvironment = webHostEnvironment;
            _userManager = userManager;
            _jwtSettings = jwtSettings;
            _dbContext = dbContext;
        }

        public async Task<IComonResponse<string>> LoginAsync(string email, string password)
        {
            try
            {
                var authUser = await _userManager.FindByEmailAsync(email);

                if (authUser == null)
                {
                    return new NotFound<string>("user is not register!");
                }

                var isValidPassword = await _userManager.CheckPasswordAsync(authUser, password);

                if (!isValidPassword)
                {
                    return new BadRequest<string>("user or password is ivalid!");
                }

                return await GenerateAuthResultForUser(authUser);
            }
            catch (Exception ex)
            {
                return new BadRequest<string>(ex.Message);
            }
        }

        public async Task<IComonResponse<string>> RegisterAsync(string email, string password, string name)
        {
            try
            {
                var isUser = await _userManager.FindByEmailAsync(email);

                if (isUser != null)
                {
                    return new BadRequest<string>("user alrady registered!");
                }

                var newUser = new ApplicationUser()
                {
                    Email = email,
                    UserName = name,
                };

                var createdUser = await _userManager.CreateAsync(newUser, password);

                if (!createdUser.Succeeded)
                {
                    return new BadRequest<string>("user can't register!");
                }

                return await GenerateAuthResultForUser(newUser);
            }
            catch (Exception ex)
            {
                return new BadRequest<string>(ex.Message);
            }
        }

        public async Task<IComonResponse<GetUserModel>> GetuserDataAsync(string userId)
        {
            try
            {
                var user = await _dbContext.Users.FirstOrDefaultAsync(o => o.Id == userId);

                if (user == null)
                {
                    return new BadRequest<GetUserModel>("Can't find user");
                }

                //List<string> userRoleIds = await _dbContext.UserRoles.Where(o => o.UserId == id).Select(o => o.RoleId).ToListAsync();


                var res = new GetUserModel
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    EmailConfirmed = user.EmailConfirmed,
                    PhoneNumber = user.PhoneNumber,
                    PhoneNumberConfirmed = user.PhoneNumberConfirmed,
                    AvatarUrl = user.AvatarUrl,
                    //UserRoles = userRoleIds
                };

                return new ComonResponse<GetUserModel>(res);
            }
            catch (Exception ex)
            {
                return new BadRequest<GetUserModel>(ex.Message);
            }
        }
        public async Task<IComonResponse<List<GetUserModel>>> GetAllUsersAsync()
        {
            try
            {
                var res = await _dbContext.Users.ToListAsync();

                var usersList = res.Select(o => new GetUserModel
                {
                    Id = o.Id,
                    UserName = o.UserName,
                    Email = o.Email,
                    EmailConfirmed = o.EmailConfirmed,
                    PhoneNumber = o.PhoneNumber,
                    PhoneNumberConfirmed = o.PhoneNumberConfirmed,
                    UserRoles = _dbContext.UserRoles.Where(i => i.UserId == o.Id).Select(i => i.RoleId).ToList()

                }).ToList();

                return new ComonResponse<List<GetUserModel>>(usersList);
            }
            catch (Exception ex)
            {
                return new BadRequest<List<GetUserModel>>(ex.Message);
            }
        }

        public async Task<IComonResponse<GetUserModel>> GetUserByIdAsync(string id)
        {
            try
            {
                var user = await _dbContext.Users.FirstOrDefaultAsync(o => o.Id == id);

                if (user == null)
                {
                    return new BadRequest<GetUserModel>("Can't find user");
                }

                List<string> userRoleIds = await _dbContext.UserRoles.Where(o => o.UserId == id).Select(o => o.RoleId).ToListAsync();

                var res = new GetUserModel
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    EmailConfirmed = user.EmailConfirmed,
                    PhoneNumber = user.PhoneNumber,
                    PhoneNumberConfirmed = user.PhoneNumberConfirmed,
                    AvatarUrl = user.AvatarUrl,
                    UserRoles = userRoleIds
                };

                return new ComonResponse<GetUserModel>(res);
            }
            catch (Exception ex)
            {
                return new BadRequest<GetUserModel>(ex.Message);
            }
        }
        public async Task CheckAdminUserAsyncInBG()
        {
            if (!_dbContext.Users.Any())
            {
                await RegisterAsync("admin@gmail.com", "Admin@123", "Admin");
            }
        }

        private async Task<IComonResponse<string>> GenerateAuthResultForUser(ApplicationUser user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var logedUserRoles = from userRole in _dbContext.UserRoles
                                 join role in _dbContext.Roles
                                 on userRole.UserId equals user.Id
                                 select role;
            var userClaims = await _userManager.GetRolesAsync(user);

            if (userClaims.Any())
            {
                foreach (var role in userClaims)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new AuthResponse<string>(tokenHandler.WriteToken(token), "Success Loged In");
        }

        public async Task<IComonResponse<UpdateCurrentUserModel>> UpdatedCurrentuserAsync(string userId, UpdateCurrentUserModel data)
        {
            try
            {
                var currentUser = await _dbContext.Users.FirstOrDefaultAsync(o => o.Id == userId);

                if(currentUser == null)
                {
                    return new BadRequest<UpdateCurrentUserModel>("user not found");
                }

                var avatarUrl = currentUser.AvatarUrl;

                if(data?.Avatar?.Length > 0)
                {
                    var directoryPath = Path.Combine(_webHostEnvironment.ContentRootPath, "UploadFiles");
                    var filePath = Path.Combine(directoryPath, data.Avatar.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        data.Avatar.CopyTo(stream);
                    }
                    avatarUrl = $"https://localhost:44353/UploadFiles/{data.Avatar.FileName}";
                } else if(data.DeleteAvatar && !string.IsNullOrEmpty(avatarUrl))
                {
                    var directoryPath = Path.Combine(_webHostEnvironment.ContentRootPath, "UploadFiles");
                    var fileName = avatarUrl.Split('/')[avatarUrl.Split('/').Length-1];
                    var filePath = Path.Combine(directoryPath, fileName);
                    File.Delete(filePath);
                    avatarUrl = "";
                }

                currentUser.UserName = data.UserName;
                currentUser.Email = data.Email;
                currentUser.PhoneNumber = data.PhoneNumber;
                currentUser.AvatarUrl = avatarUrl;

                _dbContext.SaveChanges();

                return new ComonResponse<UpdateCurrentUserModel>(data);
            }
            catch (Exception ex)
            {
                return new BadRequest<UpdateCurrentUserModel>(ex.Message);
            }
        }
    }
}
