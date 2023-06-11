using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopWebApi.Model;

namespace ShopWebApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class UserManagerController : ControllerBase
    {


        private readonly AppDbContext _context;


        //从ioc容器中拿到DbContext对象
        /// <summary>
        /// </summary>
        /// <param name="context"></param>
        public UserManagerController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="userManager"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Regist([FromBody] UserManager userManager)
        {
            _context.Entry(userManager).State = Microsoft.EntityFrameworkCore.EntityState.Added;
           
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
            return Ok();
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="userManager"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserManager userManager)
        {
          var entity = await _context.UserManagers.Where(p=>p.UserName==userManager.UserName&&p.PassWd==userManager.PassWd).FirstOrDefaultAsync();
            if (entity == null)
            {
                return BadRequest();
            }
            return Ok(entity);
        }
    }
}
