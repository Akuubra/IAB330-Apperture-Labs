using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glados.Core.Interfaces
{
    /// <summary>
    /// Interface for Contacts Observable Collection. Exists only so that ContactLabel and ContactWrapper can both implement it
    /// and thus both be added to the Oberservable Collection
    /// </summary>
    public interface IContactListType
    {

        string Label { get; set; }
    }
}
