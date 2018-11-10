/**
 * That Modelrepresents domain specific data and business logic in MVC architecture 
 * Model class holds data in public properties. All the Model classes reside in the Model folder in MVC folder structure.
 */


namespace CiteApp_v1._2.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Attribute
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Attribute()
        {
            this.Employees = new HashSet<Employee>();
        }
    
        public System.Guid ATTR_ID { get; set; }
        public System.Guid ATTR_EMP_ID { get; set; }
        public string ATTR_Name { get; set; }
        public string ATTR_Value { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
