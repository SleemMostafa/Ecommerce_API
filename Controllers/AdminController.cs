using Ecommerce_API.DTO;
using Ecommerce_API.Helper;
using Ecommerce_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
         private readonly ApplicationContext context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration configuration;

        public AdminController(UserManager<ApplicationUser> userManager,
                                 IConfiguration configuration,
                                RoleManager<IdentityRole> _roleManager,
                                ApplicationContext _context)
        {
            this.userManager = userManager;
            this.roleManager = _roleManager;
            this.context = _context;
            this.configuration = configuration;
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDto newUserRegister)
        {
            if(ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            //save in database
            //Step one Create User
            ApplicationUser userModel = new ApplicationUser();
            userModel.UserName = newUserRegister.UserName;
            userModel.Address.City = newUserRegister.Address.City;
            userModel.Address.Street = newUserRegister.Address.Street;
            userModel.Address.PostalCode = newUserRegister.Address.PostalCode;
            userModel.Day = newUserRegister.Day;
            userModel.SpecfiyDay = newUserRegister.SpecfiyDay;
            foreach (var mobile in newUserRegister.PhoneNumber)
            {
                userModel.Mobiles.Add(new CustomMobile() { Mobile = mobile });
            }
            userModel.Email = newUserRegister.Email;
            userModel.PasswordHash = newUserRegister.Password;
            IdentityResult  identityResult = await userManager.CreateAsync(userModel, newUserRegister.Password);
            if(identityResult.Succeeded)
            {
                return Ok(new {identityResult.Succeeded,newUserRegister.UserName,newUserRegister.Email,newUserRegister.PhoneNumber});
            }
            else
            {
                foreach (var item in identityResult.Errors)
                {
                    ModelState.AddModelError("",item.Description);
                }
                return BadRequest(ModelState);
            }

        }
        [Authorize(Roles = "Admin")]
        [HttpGet("Users")]
        public IActionResult GetAll()
        {
           
            var users = context.Users.ToList();
            if (users != null)
            {
                List<UserDetails> userDetails = new List<UserDetails>();
                foreach (var user in users)
                {
                    userDetails.Add(new UserDetails { Id = user.Id ,UserName = user.UserName,Email=user.Email});
                }
                return Ok(userDetails);
            }
            return BadRequest(ModelState);
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto userLogin)
        {
            if(ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            //check about data user Entring
            ApplicationUser userModel  = await userManager.FindByNameAsync(userLogin.UserName);
            if(userModel != null)
            { 
                //check his has same password
                if(await userManager.CheckPasswordAsync(userModel,userLogin.Password)==true)
                {
                    //create Token base on Claims claim is info want add in token
                    // must add with custome ifno jti ==> uniq key type of "string"  of token 
                    var claims = new List<Claim>();
                    claims.Add(new Claim("uId",userModel.Id));//custome claim
                    claims.Add(new Claim(ClaimTypes.Name , userModel.UserName));
                    claims.Add(new Claim(ClaimTypes.NameIdentifier , userModel.Id));
                    claims.Add(new Claim(ClaimTypes.Email , userModel.Email));
                    IList<string> roles = await userManager.GetRolesAsync(userModel); 
                    claims.Add(new Claim(ClaimTypes.Role , userModel.Id));
                    if (roles.Count < 1)
                    {
                        return BadRequest("Your not admin");
                    }

                    foreach (var role in roles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role));
                   
                    }

                    //jti ==> id for token use in search 
                    // has anther namespace if ex error for json
                    claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));//guid for not repat same token 

                    //-------------------------- Add In Token ------------------------------------//
                    //Created token
                    //JWT jWT = new JWT();
                    var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]));
                    var mtToken = new JwtSecurityToken( // in this do token in shap json only 
                        //all this add in body payload
                        audience:configuration["JWT:Audience"],
                        issuer: configuration["JWT:Issure"],
                        expires:DateTime.Now.AddHours(1),
                        claims:claims,
                        //to virfid to sign in 
                        signingCredentials: new SigningCredentials(Key,SecurityAlgorithms.HmacSha256) // this line orgainze info add in token but not create token
                        );
                    //to convert token to copacto for send in body or in header 

                    
                    return Ok(new
                    {
                        token=new JwtSecurityTokenHandler().WriteToken(mtToken),

                        expiration= mtToken.ValidTo,
                       
                       
                       
                    });
                }
                else
                {
                    //return BadRequest("User Name And Password Not Valid");
                    return Unauthorized();//401
                }
            }
            return Unauthorized();  
            }
        //[Authorize(Roles ="Admin")]
        //[HttpPost("addrole")]
        //public async Task<IActionResult> AddRole(AddRoleModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    var result = await HelpAddRole(model);
        //    if (!string.IsNullOrEmpty(result))
        //    {
        //        return BadRequest(result);
        //    }
        //    return Ok(model);
        //}
        //[NonAction]
        //public async Task<string> HelpAddRole(AddRoleModel addRole)
        //{
        //    ApplicationUser user = await userManager.FindByIdAsync(addRole.UserId);
        //    if (user is null || !await roleManager.RoleExistsAsync(addRole.RoleName))
        //    {
        //        return "Role or user name is invalid";
        //    }
        //    if (await userManager.IsInRoleAsync(user, addRole.RoleName))
        //    {
        //        return "User already assigned to this role";
        //    }
        //    var result = await userManager.AddToRoleAsync(user, addRole.RoleName);
        //    if (result.Succeeded)
        //    {
        //        return String.Empty;
        //    }
        //    return "Sonething went wrong";
        //}
    }
}

