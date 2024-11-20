using Core.Domain;
using Core.DomainServices.Services.Intf;

namespace GraphQLServer.GraphQL {
    public class Mutation {
        private readonly IPackageService _packageService;

        public Mutation(IPackageService packageService) {
            _packageService = packageService;
        }
        public IEnumerable<Package> GetPackages() => _packageService.GetAll();
        public Package GetPackage(int id) => _packageService.GetDetails(id);
    }
}
