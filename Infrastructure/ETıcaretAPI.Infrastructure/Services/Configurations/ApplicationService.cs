using ETıcaretAPI.Application.Abstractions.Services.Configurations;
using ETıcaretAPI.Application.CustomAttributes;
using ETıcaretAPI.Application.Dtos.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ETıcaretAPI.Infrastructure.Services.Configurations
{
    internal class ApplicationService : IApplicationService
    {
        public List<Menu> GetAuthorizeDefinitionEndpoints(Type type)
        {
            Assembly assembly = Assembly.GetAssembly(type);
            List<Menu> menus = new List<Menu>();
            var controllers = assembly.GetTypes().Where(t => t.IsAssignableTo(typeof(ControllerBase))); // controllerBase den miras alan veriler yani controllerlar
            if (controllers != null)
            {
                foreach (var controller in controllers)
                {
                    var actions = controller.GetMethods().Where(m => m.IsDefined(typeof(AuthorizeDefinitionAttribute))); // AuthorizeDefinitionAttribute ile tanımlanmış methodlar
                    if (actions != null)
                    {
                        foreach (var action in actions)
                        {
                          var attributes=action.GetCustomAttributes(true); //custom attributeleri getir 
                            if(attributes != null)
                            {
                                Menu menu = null;
                               var authorizeDefinitionAttribute = attributes.FirstOrDefault(a => a.GetType() == typeof(AuthorizeDefinitionAttribute)) as AuthorizeDefinitionAttribute; // custom attrivutelerden type ı AuthorizeDefinitionAttribute olanı al 
                                if (!menus.Any(m=>m.Name== authorizeDefinitionAttribute.Menu)) // menus AuthorizeDefinitionAttribute içerisindeki Menu(controller adı) içermiyorsa devam et 
                                {
                                    menu = new() { Name = authorizeDefinitionAttribute.Menu }; // menu içerisindeki name propuna authorizeDefinitionAttribute.Menu değişkenini ata 
                                    menus.Add(menu);  //menus içerisine ekle 
                                }
                                else
                                {
                                    menu = menus.FirstOrDefault(m => m.Name == authorizeDefinitionAttribute.Menu); // eğer zaten menus içerisinde değer varsa ata
                                }
                                Application.Dtos.Configuration.Action _action = new() // actin referansı yarat
                                {
                                    ActionType=Enum.GetName(authorizeDefinitionAttribute.ActionType), // action type propuna Enum.GetName(authorizeDefinitionAttribute.ActionType) sayısal enum değerinin valuesini ata 
                                    Definition =authorizeDefinitionAttribute.Definition, // tanım olarak authorizeDefinitionAttribute.Definition değerini ata

                                };
                                var httpAttribute= attributes.FirstOrDefault(a => a.GetType().IsAssignableTo(typeof(HttpMethodAttribute))) as HttpMethodAttribute; // attributelardan HttpMethodAttribute miras alan atribute yi getirir.

                                if (httpAttribute != null)                            
                                    _action.HttpType=httpAttribute.HttpMethods.First();  // get,post,put,delete değerleri hangisi ise _action.HttpType değerine atanır                         
                                else                               
                                    _action.HttpType = HttpMethods.Get;
                                
                                _action.Code =$"{ _action.HttpType}.{_action.ActionType}.{_action.Definition.Replace(" ","")}"; // yaratılan kod ise httpType.actionTypr.definition olarak atanır ve menu.actions propuna atanır.
                                menu.Actions.Add(_action);
                            }
                        }
                    }
                }
            }
            return menus;
        }
    }
}
