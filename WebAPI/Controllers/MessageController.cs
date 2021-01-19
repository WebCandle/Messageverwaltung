using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        public MessageController()
        {
            Db = new Db();
        }

        public Db Db { get; set; }
        // GET: api/<MessageController>
        [HttpGet]
        public IEnumerable<Message> Get()
        {
            return Db.Messages.ToList();
        }

        // GET api/<MessageController>/5
        [HttpGet("{id}")]
        public ActionResult<Message> Get(int id)
        {
            var person = Db.Messages.Where(x => x.MessageId == id);
            if (person == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(person);
            }
        }

        // POST api/<MessageController>
        [HttpPost]
        public IActionResult Post([FromBody] Message message)
        {
            if (message == null)
            {
                return this.BadRequest();
            }
            else
            {
                Db.Messages.Add(message);
                int state = Db.SaveChanges();
                return Ok(state);
            }
        }

        // PUT api/<MessageController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Message message_new)
        {
            Message message = Db.Messages.Where(x => x.MessageId == id).First();
            if (message == null)
            {
                return NotFound();
            }
            else
            {
                message.Text = message_new.Text;
                message.Date= message_new.Date;
                int state = Db.SaveChanges();
                return Ok(state);
            }
        }

        // DELETE api/<MessageController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Message message = Db.Messages.Where(x => x.MessageId == id).First();
            if (message == null)
            {
                return NotFound();
            }
            else
            {
                Db.Messages.Remove(message);
                int state = Db.SaveChanges();
                return Ok(state);
            }
        }
    }
}
