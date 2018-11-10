/**
 * That Modelrepresents domain specific data and business logic in MVC architecture 
 * Model class holds data in public properties. All the Model classes reside in the Model folder in MVC folder structure.
 */
namespace CiteApp_v1._2.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Employee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Employee()
        {
            this.Employee1 = new HashSet<Employee>();
            this.Attributes = new HashSet<Attribute>();
        }
    
        public System.Guid EMP_ID { get; set; }
        public string EMP_Name { get; set; }
        public System.DateTime EMP_DateOfHire { get; set; }
        public Nullable<System.Guid> EMP_Supervisor { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Employee> Employee1 { get; set; }
        public virtual Employee Employee2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Attribute> Attributes { get; set; }
    }
}
