using System;
using Core.Domain;
using Xunit;

namespace Domain.Tests
{
    public class DomainTests
    {
        [Fact]
        public void Birthday_Cannot_Be_Set_In_The_Future()
        {
            // Arrange
            var exceptionHasBeenThrown = false;
            var student = new Student { Id = 1, Name = "firststudent@hotmail.com", StudentNumber = "2142135", Emailadres = "firststudent@hotmail.com", StudyCity = CityEnum.Breda, Phonenumber = "0612345611" };

            try
            {
                // Act
                student.Birthdate = new DateTime(5000, 12, 26, 12, 30, 00);
            }
            catch (InvalidOperationException)
            {
                exceptionHasBeenThrown = true;
            }
            // Assert
            Assert.True(exceptionHasBeenThrown);
        }

        [Fact]
        public void Birthday_Minimum_Age_Sixteen()
        {
            // Arrange
            var exceptionHasBeenThrown = false;
            var student = new Student { Id = 1, Name = "firststudent@hotmail.com", StudentNumber = "2142135", Emailadres = "firststudent@hotmail.com", StudyCity = CityEnum.Breda, Phonenumber = "0612345611" };

            try
            {
                // Act
                student.Birthdate = new DateTime(2022, 12, 26, 12, 30, 00);
            }
            catch (InvalidOperationException)
            {
                exceptionHasBeenThrown = true;
            }
            // Assert
            Assert.True(exceptionHasBeenThrown);
        }

        [Fact]
        public void Birthday_Can_Be_Created()
        {
            // Arrange
            var exceptionHasBeenThrown = false;
            var student = new Student { Id = 1, Name = "firststudent@hotmail.com", StudentNumber = "2142135", Emailadres = "firststudent@hotmail.com", StudyCity = CityEnum.Breda, Phonenumber = "0612345611" };

            try
            {
                // Act
                student.Birthdate = new DateTime(1995, 12, 26, 12, 30, 00);
            }
            catch (InvalidOperationException)
            {
                exceptionHasBeenThrown = true;
            }
            // Assert
            Assert.False(exceptionHasBeenThrown);
        }
    }
}