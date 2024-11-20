using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace Portal {
    public class IdentitySeedData {
        private const string employeeUser = "firstemployee@hotmail.com";
        private const string employeePassword = "Secret123$";

        private const string employeeUser2 = "secondemployee@hotmail.com";
        private const string employeePassword2 = "Secret123$";

        private const string studentUser = "firststudent@hotmail.com";
        private const string studentPassword = "Secret123$";

        private const string studentUser2 = "secondstudent@hotmail.com";
        private const string studentPassword2 = "Secret123$";

        private const string studentUser3 = "thirdstudent@hotmail.com";
        private const string studentPassword3 = "Secret123$";

        public static async Task EnsurePopulated(UserManager<IdentityUser> userManager) {
            IdentityUser employee = await userManager.FindByIdAsync(employeeUser);
            if (employee == null) {
                employee = new IdentityUser(employeeUser);
                await userManager.CreateAsync(employee, employeePassword);
                await userManager.AddClaimAsync(employee, new Claim("CanteenEmployee", "true"));
                await userManager.SetEmailAsync(employee, employeeUser);
            }

            IdentityUser employee2 = await userManager.FindByIdAsync(employeeUser2);
            if (employee2 == null) {
                employee2 = new IdentityUser(employeeUser2);
                await userManager.CreateAsync(employee2, employeePassword2);
                await userManager.AddClaimAsync(employee2, new Claim("CanteenEmployee", "true"));
                await userManager.SetEmailAsync(employee2, employeeUser2);
            }

            IdentityUser student = await userManager.FindByIdAsync(studentUser);
            if (student == null) {
                student = new IdentityUser(studentUser);
                await userManager.CreateAsync(student, studentPassword);
                await userManager.AddClaimAsync(student, new Claim("Student", "true"));
                await userManager.SetEmailAsync(student, studentUser);
            }

            IdentityUser student2 = await userManager.FindByIdAsync(studentUser2);
            if (student2 == null) {
                student2 = new IdentityUser(studentUser2);
                await userManager.CreateAsync(student2, studentPassword2);
                await userManager.AddClaimAsync(student2, new Claim("Student", "true"));
                await userManager.SetEmailAsync(student2, studentUser2);
            }

            IdentityUser student3 = await userManager.FindByIdAsync(studentUser3);
            if (student3 == null) {
                student3 = new IdentityUser(studentUser3);
                await userManager.CreateAsync(student3, studentPassword3);
                await userManager.AddClaimAsync(student3, new Claim("Student", "true"));
                await userManager.SetEmailAsync(student3, studentUser3);
            }
        }
    }
}