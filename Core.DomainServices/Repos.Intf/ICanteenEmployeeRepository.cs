using System;
using Core.Domain;

namespace Core.DomainServices.Repos.Intf {
    public interface ICanteenEmployeeRepository {
        IEnumerable<CanteenEmployee> GetCanteenEmployees();
        CanteenEmployee GetById(int id);
        CanteenEmployee GetByName(string name);
    }
}
