//------------------------------------------------------------------------------
// <auto-generated>
//    Dieser Code wurde aus einer Vorlage generiert.
//
//    Manuelle Änderungen an dieser Datei führen möglicherweise zu unerwartetem Verhalten Ihrer Anwendung.
//    Manuelle Änderungen an dieser Datei werden überschrieben, wenn der Code neu generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TinkerLib
{
    using System;
    using System.Collections.Generic;
    
    public partial class Sensors
    {
        public Sensors()
        {
            this.Values = new HashSet<Values>();
        }
    
        public int idSensoren { get; set; }
        public int Devices_idDevices { get; set; }
        public string Name { get; set; }
        public string Measure { get; set; }
    
        public virtual Devices Devices { get; set; }
        public virtual ICollection<Values> Values { get; set; }
    }
}
