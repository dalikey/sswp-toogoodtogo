using Microsoft.AspNetCore.Mvc;

namespace GraphQLServer.GraphQL {
    public class RouteController {
        [HttpGet("GetGraphQLRoute")]
        public string Get() {
            return "GraphQL route is https://localhost:7047/graphql/ or https://avansgraphql.azurewebsites.net/graphql/\nSwitch from Schema Reference to > Operations";
        }
    }
}
