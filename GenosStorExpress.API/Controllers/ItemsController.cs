using GenosStorExpress.Application.Service.Interface.Entity.Items;
using GenosStorExpress.Application.Wrappers.Entity.Item;
using GenosStorExpress.Application.Wrappers.Enum;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GenosStorExpress.API.Controllers
{
    [Route("api/items")]
    [ApiController]
    public class ItemsController : ControllerBase {
        
        private readonly IItemServiceRouter _itemServiceRouter;
        private readonly IItemTypeService _itemTypeService;

        public ItemsController(IItemServiceRouter itemServiceRouter, IItemTypeService itemTypeService) {
            _itemServiceRouter = itemServiceRouter;
            _itemTypeService = itemTypeService;
        }
        
        [HttpGet("{type}")]
        public ActionResult<IEnumerable<AnonymousItemWrapper>> List(string type) {
            ItemTypeDescriptor descriptor = _itemTypeService.GetDescriptor(type);

            if (descriptor == ItemTypeDescriptor.Unknown) {
                return BadRequest($"Неизвестный тип товара - {type}");
            }

            try {
                IEnumerable<AnonymousItemWrapper> list = _itemServiceRouter.List(descriptor);
                return Ok(list);
            } catch (Exception e) {
                return BadRequest($"Произошла ошибка - {e.Message}");
            }
        }
        
        [HttpGet("{type}/{id:int}")]
        public ActionResult<AnonymousItemWrapper> Get(string type, int id) {
            ItemTypeDescriptor descriptor = _itemTypeService.GetDescriptor(type);
            
            if (descriptor == ItemTypeDescriptor.Unknown) {
                return BadRequest($"Неизвестный тип товара - {type}");
            }
            
            
            return _itemServiceRouter.Get(descriptor, id);
        }
        
        [HttpPost]
        public ActionResult<AnonymousItemWrapper> Create([FromBody]AnonymousItemWrapper value) {
            ItemTypeDescriptor descriptor = _itemTypeService.GetDescriptor(value.ItemType);
            _itemServiceRouter.Create(descriptor, value);
            _itemServiceRouter.Save(descriptor);
            return CreatedAtAction(nameof(Create), new { id = ((AnonymousItemWrapper) value).Id }, value);
        }
        
        [HttpPut("{id:int}")]
        public IActionResult Update(string type, int id, [FromBody]AnonymousItemWrapper value) {
            ItemTypeDescriptor descriptor = _itemTypeService.GetDescriptor(type);
            if (id != value.Id) return BadRequest();
            _itemServiceRouter.Update(descriptor, id, value);
            _itemServiceRouter.Save(descriptor);
            return NoContent();
        }
        
        [HttpDelete("{type}/{id:int}")]
        public IActionResult Delete(string type, int id) {
            ItemTypeDescriptor descriptor = _itemTypeService.GetDescriptor(type);
            _itemServiceRouter.Delete(descriptor, id);
            _itemServiceRouter.Save(descriptor);
            return NoContent();
        }
    }
}
