
using System.Collections.Generic;
using System.Linq;

namespace SchoolOfNet_api_rest_asp_net_core_hateoas.HATEOAS
{
    /***
    Classe que representa os LINKS do HEATEOAS
     */
    public class Link 
    {
        public Link(string href, string rel, string method){
            this.href = href;
            this.rel = rel;
            this.method = method;
        }

        public string href { get; set; }    
        public string rel { get; set; } 
        public string method { get; set; }  
    }
}