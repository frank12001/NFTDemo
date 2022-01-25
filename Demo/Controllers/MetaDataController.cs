using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MetaDataController : ControllerBase
    {
        [HttpGet("{tokenurl}")]
        public IActionResult Get(int tokenurl)
        {
            return new JsonResult(new Dto()
            {
                Name = "CQINFT1",
                Title = "NFTTitle",
                Type = "object",
                Image = "http://testnft.cqiserv.com/testnft/1.png",
                External_url = "http://testnft.cqiserv.com/",
                Description = "CQI Test",
                Attributes = new Attribute[]
                 {
                     new Attribute() { Trait_type = "level", Value = "10" },
                     new Attribute() { Trait_type = "Hand", Value = "hand1" }
                 },
                Properties = new Properties()
                {
                    Name = new Propertie() { Type = "string", Description = "CQINFT1Name" },
                    Description = new Propertie() { Type = "string", Description = "Good Good" },
                    Image = new Propertie() { Type = "string", Description = "http://testnft.cqiserv.com/testnft/1.png" }
                },
            });
        }

        public class Dto
        {
            public Attribute[] Attributes { get; set; }
            public Properties Properties { get; set; }
            public string Description { get; set; }
            public string External_url { get; set; }
            public string Image { get; set; }
            public string Name { get; set; }
            public string Type { get; set; }
            public string Title { get; set; }


        }
        public class Attribute
        {
            public string Trait_type { get; set; }
            public string Value { get; set; }
        }

        public class Properties
        {
            public Propertie Name { get; set; }
            public Propertie Description { get; set; }
            public Propertie Image { get; set; }
        }

        public class Propertie
        {
            public string Type { get; set; }
            public string Description { get; set; }
        }
    }
}
