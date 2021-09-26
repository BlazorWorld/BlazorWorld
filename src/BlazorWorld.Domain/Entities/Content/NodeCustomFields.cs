using BlazorHero.CleanArchitecture.Domain.Contracts;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorWorld.Domain.Entities.Content
{
    public class NodeCustomFields : AuditableEntity<string>
    {
        [ForeignKey("Node")]
        public string NodeId { get; set; }
        public virtual Node Node { get; set; }
        public string CustomField1 { get; set; }
        public string CustomField2 { get; set; }
        public string CustomField3 { get; set; }
        public string CustomField4 { get; set; }
        public string CustomField5 { get; set; }
        public string CustomField6 { get; set; }
        public string CustomField7 { get; set; }
        public string CustomField8 { get; set; }
        public string CustomField9 { get; set; }
        public string CustomField10 { get; set; }
        public string CustomField11 { get; set; }
        public string CustomField12 { get; set; }
        public string CustomField13 { get; set; }
        public string CustomField14 { get; set; }
        public string CustomField15 { get; set; }
        public string CustomField16 { get; set; }
        public string CustomField17 { get; set; }
        public string CustomField18 { get; set; }
        public string CustomField19 { get; set; }
        public string CustomField20 { get; set; }
        public string IndexedCustomField1 { get; set; }
        public string IndexedCustomField2 { get; set; }
        public string IndexedCustomField3 { get; set; }
        public string IndexedCustomField4 { get; set; }
        public string IndexedCustomField5 { get; set; }
        public string IndexedCustomField6 { get; set; }
        public string IndexedCustomField7 { get; set; }
        public string IndexedCustomField8 { get; set; }
        public string IndexedCustomField9 { get; set; }
        public string IndexedCustomField10 { get; set; }
        public string IndexedCustomField11 { get; set; }
        public string IndexedCustomField12 { get; set; }
        public string IndexedCustomField13 { get; set; }
        public string IndexedCustomField14 { get; set; }
        public string IndexedCustomField15 { get; set; }
        public string IndexedCustomField16 { get; set; }
        public string IndexedCustomField17 { get; set; }
        public string IndexedCustomField18 { get; set; }
        public string IndexedCustomField19 { get; set; }
        public string IndexedCustomField20 { get; set; }
    }
}
