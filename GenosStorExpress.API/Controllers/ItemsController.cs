using GenosStorExpress.Application.Service.Interface.Entity.Items;
using GenosStorExpress.Application.Wrappers.Entity.Item;
using GenosStorExpress.Application.Wrappers.Enum;
using GenosStorExpress.Utils;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GenosStorExpress.API.Controllers {
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
                return BadRequest(new DetailObject($"Неизвестный тип товара - {type}"));
            }

            try {
                IEnumerable<AnonymousItemWrapper> list = _itemServiceRouter.List(descriptor);
                return Ok(list);
            } catch (NullReferenceException e) {
                return BadRequest(new DetailObject(e.Message));
            } catch (Exception e) {
                return BadRequest(new DetailObject($"Произошла ошибка - {e.Message}"));
            }
            
        }
        
        [HttpGet("{type}/{id:int}")]
        public ActionResult<AnonymousItemWrapper> Get(string type, int id) {
            
            ItemTypeDescriptor descriptor = _itemTypeService.GetDescriptor(type);
            
            if (descriptor == ItemTypeDescriptor.Unknown) {
                return BadRequest(new DetailObject($"Неизвестный тип товара - {type}"));
            }
            
            try {
                AnonymousItemWrapper item = _itemServiceRouter.Get(descriptor, id);
                return Ok(item);
            } catch (NullReferenceException e) {
                return BadRequest(new DetailObject(e.Message));
            } catch (Exception e) {
                return BadRequest(new DetailObject($"Произошла ошибка - {e.Message}"));
            }

        }
        
        [HttpPost]
        public ActionResult<AnonymousItemWrapper> Create([FromBody]AnonymousItemWrapper value) {
            
            ItemTypeDescriptor descriptor = _itemTypeService.GetDescriptor(value.ItemType);
            
            if (descriptor == ItemTypeDescriptor.Unknown) {
                return BadRequest(new DetailObject($"Неизвестный тип товара - {value.ItemType}"));
            }

            try {
                _itemServiceRouter.Create(descriptor, value);
                _itemServiceRouter.Save(descriptor);
                return CreatedAtAction(nameof(Create), new { id = value.Id }, value);
            } catch (NullReferenceException e) {
                return BadRequest(new DetailObject(e.Message));
            } catch (Exception e) {
                return BadRequest(new DetailObject($"Произошла ошибка - {e.Message}"));
            }
            
        }
        
        [HttpPut("{id:int}")]
        public IActionResult Update(int id, [FromBody]AnonymousItemWrapper value) {
            
            ItemTypeDescriptor descriptor = _itemTypeService.GetDescriptor(value.ItemType);
            
            if (descriptor == ItemTypeDescriptor.Unknown) {
                return BadRequest(new DetailObject($"Неизвестный тип товара - {value.ItemType}"));
            }

            if (id != value.Id) {
                return BadRequest(new DetailObject("Ошибка - попытка изменить номер товара"));
            }

            try {
                _itemServiceRouter.Update(descriptor, id, value);
                _itemServiceRouter.Save(descriptor);
                return NoContent();
            } catch (NullReferenceException e) {
                return BadRequest(new DetailObject(e.Message));
            } catch (Exception e) {
                return BadRequest(new DetailObject($"Произошла ошибка - {e.Message}"));
            }
            
        }
        
        [HttpDelete("{type}/{id:int}")]
        public IActionResult Delete(string type, int id) {
            ItemTypeDescriptor descriptor = _itemTypeService.GetDescriptor(type);
            
            if (descriptor == ItemTypeDescriptor.Unknown) {
                return BadRequest(new DetailObject($"Неизвестный тип товара - {type}"));
            }
            
            try {
                _itemServiceRouter.Delete(descriptor, id);
                _itemServiceRouter.Save(descriptor);
                return NoContent();
            } catch (NullReferenceException e) {
                return BadRequest(new DetailObject(e.Message));
            } catch (Exception e) {
                return BadRequest(new DetailObject($"Произошла ошибка - {e.Message}"));
            }
            
        }
    }
}
