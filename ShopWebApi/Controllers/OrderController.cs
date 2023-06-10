﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopWebApi.Model;

namespace ShopWebApi.Controllers
{
    [ApiController]
    [Route("api/{controller}")]
    public class OrderController : ControllerBase
    {
        [Route("api/[controller]")]
        [ApiController]
        public class OrdersController : ControllerBase
        {
            private readonly AppDbContext _context;

            public OrdersController(AppDbContext context)
            {
                _context = context;
            }

            // GET: api/Orders
            [HttpGet]
            public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
            {
                return await _context.Orders.ToListAsync();
            }

            // GET: api/Orders/5
            [HttpGet("{id}")]
            public async Task<ActionResult<Order>> GetOrder(int id)
            {
                var order = await _context.Orders.FindAsync(id);

                if (order == null)
                {
                    return NotFound();
                }

                return order;
            }

            // PUT: api/Orders/5
            [HttpPut("{id}")]
            public async Task<IActionResult> PutOrder(int id, Order order)
            {
                if (id != order.OrderID)
                {
                    return BadRequest();
                }

                _context.Entry(order).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(id))
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

            // POST: api/Orders
            [HttpPost]
            public async Task<ActionResult<Order>> PostOrder(Order order)
            {
                _context.Orders.Add(order);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetOrder), new { id = order.OrderID }, order);
            }

            // DELETE: api/Orders/5
            [HttpDelete("{id}")]
            public async Task<ActionResult<Order>> DeleteOrder(int id)
            {
                var order = await _context.Orders.FindAsync(id);
                if (order == null)
                {
                    return NotFound();
                }

                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();

                return order;
            }

            private bool OrderExists(int id)
            {
                return _context.Orders.Any(e => e.OrderID == id);
            }
        }
    }
}
