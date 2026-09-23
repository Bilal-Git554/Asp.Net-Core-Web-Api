using Library_Management.Connection;
using Library_Management.Entities;
using Library_Management.IRepository;
using Library_Management.Report;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library_Management.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _IBook;
        public BooksController(IBookRepository IBook)
        {
            _IBook = IBook;
        }//Dependency Injection For The Database Connection

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> Read_Data()
        {
            var Read = await _IBook.Read_Data();
            if(Read == null)
            {
                return NotFound("No Exisiting Book!");
            }
            return Ok(Read);
        }//Display All The Data


        [HttpPost("read/{id}")]
        public async Task<ActionResult<Book>> Readby_Id(int id)
        {
            var Id = await _IBook.Readby_Id(id);
            if (Id == null)
            {
                return NotFound("Book Not Found!");
            }
            return Ok(Id);
        }//Return Single Data By Id


        [HttpPost]
        public async Task<ActionResult<Book>> Create_Data(Book B)
        {
            var Insert = await _IBook.Create_Data(B);
            return CreatedAtAction(nameof(Readby_Id), new { id = B.Book_Id }, B);
        }//Insert The Data


        [HttpPut("{id}")]
        public async Task<ActionResult<Book>> Update_Data(int id , Book U)
        {
            var Update = await _IBook.Update_Data(id, U);
            if(Update == null)
            {
                return NotFound("Unable To Update The Book!");
            }
            return Ok(U);

        }//Update The Data


        [HttpPost("delete/{id}")]
        public async Task<ActionResult<Book>> Delete_Data (int id)
        {
            var Delete = await _IBook.Delete_Data(id);
            if(Delete == null)
            {
                return NotFound("Unable To Delete The Book!");
            }
            return Ok(Delete);
        }//Delete The Data
    }
}
