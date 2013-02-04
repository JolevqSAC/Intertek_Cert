using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//agregamos la referencia al repositorio
using Intertek.Business.Entities;
using Intertek.DataAccess.Repository;

namespace Intertek.DataAccess
{
    public class ContactoClienteDAL : Repository<ContactoClienteDAL, ContactoCliente>
    {
    }
}
