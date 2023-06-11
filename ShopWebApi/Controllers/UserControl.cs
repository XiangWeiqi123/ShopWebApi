using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopWebApi.Model;
using System.Reflection.Metadata.Ecma335;

namespace ShopWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {

        private readonly AppDbContext _context;


        //从ioc容器中拿到DbContext对象
        /// <summary>
        /// </summary>
        /// <param name="context"></param>
        public UserController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/User
        /// <summary>
        /// 拿到所有用户
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetUserList()
        {
            return Ok(await _context.Users.Where(p => !p.IsDeleted).ToListAsync());
        }

        // GET: api/User/id
        /// <summary>
        /// 通过id查找用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> FindUserById(int id)
        {
            var user = await _context.Users.Where(u => !u.IsDeleted && u.UserID == id).Include(u => u.Orders).FirstOrDefaultAsync();

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }
        /// <summary>
        /// 把用户连着订单(订单里面包含订单详情)查出来
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("FindUserWhthOrders")]
        public async Task<IActionResult> FindUserWithOrders(int id)
        {
            var userEntity = await _context.Users.Where(p => p.UserID == id).Include(p=>p.Orders).ThenInclude(p=>p.OrderDetails).FirstOrDefaultAsync();
            if (userEntity == null || userEntity.IsDeleted)
            {
                return BadRequest();
            }
            return Ok(userEntity);
        }

        // PUT: api/User/id
        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, User user)
        {

            if (id != user.UserID)
            {
                return BadRequest();
            }

            var userChanging = await _context.Users.FirstOrDefaultAsync(p => p.UserID == id);

            if (userChanging == null || userChanging.IsDeleted)
            {
                return BadRequest();
            }

            userChanging.UserName = user.UserName;
            userChanging.Email = user.Email;
            userChanging.PhoneNumber = user.PhoneNumber;


            _context.Entry(userChanging).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Users.Any(e => e.UserID == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        // POST: api/User
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<User>> AddUser(User user)
        {
            //模型校验，确保输入的用户实体是合法的
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                return BadRequest(errors);
            }
            if (_context.Users.Any(p => p.PhoneNumber == user.PhoneNumber))
            {
                return NoContent();
            }
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(user);
        }

        // DELETE: api/User/id
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.SingleOrDefaultAsync(p => p.UserID == id);
            if (user != null)
            {
                user.IsDeleted = true;  // I assume you want to set it to true for deletion
                await _context.SaveChangesAsync();
                return NoContent();
            }

            return BadRequest();
        }

       

    }
}