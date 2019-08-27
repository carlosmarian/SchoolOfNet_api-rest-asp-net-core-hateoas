using System;
using System.Collections.Generic;

namespace SchoolOfNet_api_rest_asp_net_core_hateoas.HATEOAS
{
    public class HATEOAS
    {
        private string url;
        private string protocol = "https://";
        public List<Link> actions = new List<Link>();

        public HATEOAS(string url){
            this.url = url;
        }
        public HATEOAS(string url, string protocol){
            this.url = url;
            this.protocol = protocol;
        }

        public void AddAction(string rel, string method){
            // https://localhost:5001/api/v1/Produtos
            this.actions.Add(new Link(this.protocol+this.url, rel, method));
        }
        public Link[] GetActions(string id){
            List<Link> tmp = new List<Link>(this.actions.Count);
            foreach (var pp in this.actions)
            {
                tmp.Add(new Link(pp.href, pp.rel, pp.method));
            }
            tmp.ForEach(p => p.href+="/"+id);
            return tmp.ToArray();
        }
    }
}