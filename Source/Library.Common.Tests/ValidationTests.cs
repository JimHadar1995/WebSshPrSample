using Library.Common.ValidationHelpers;
using Xunit;

namespace Library.Common.Tests
{
    public sealed class ValidationTests
    {
        [Fact(DisplayName = "Проверка метола " + nameof(Validation.ObjectNameIsValid))]
        public void ObjectNameIsValidTest()
        {
            string validValue = "test123_-";
            Assert.True(Validation.ObjectNameIsValid(validValue));

            string invalidValue = "test123!";
            Assert.False(Validation.ObjectNameIsValid(invalidValue));

            invalidValue = "test123$";
            Assert.False(Validation.ObjectNameIsValid(invalidValue));

            invalidValue = "тест123";
            Assert.False(Validation.ObjectNameIsValid(invalidValue));
        }

        [Fact(DisplayName = "Проверка метода " + nameof(Validation.ContainsOnlyLatin))]
        public void ContainsOnlyLatinTest()
        {
            string validValue = "test123";
            Assert.True(Validation.ContainsOnlyLatin(validValue));

            string invalidValue = "test*";
            Assert.False(Validation.ContainsOnlyLatin(invalidValue));
        }

        [Fact(DisplayName = "Проверка метода " + nameof(Validation.ContainsUpperCase))]
        public void ContainsUpperCaseTest()
        {
            string validValue = "Test123";
            Assert.True(Validation.ContainsUpperCase(validValue));

            Assert.False(Validation.ContainsUpperCase("test123"));
        }

        [Fact(DisplayName = "Проверка метода " + nameof(Validation.ContainsLowerCase))]
        public void ContainsLowerCaseTest()
        {
            Assert.True(Validation.ContainsLowerCase("test1234"));
            Assert.False(Validation.ContainsLowerCase("TEST1234"));
        }

        [Fact(DisplayName = "Проверка метода " + nameof(Validation.ContainsNonAlphaNumeric))]
        public void ContainsNonAlphaNumericTest()
        {
            Assert.True(Validation.ContainsNonAlphaNumeric("Test1234%"));
            Assert.False(Validation.ContainsNonAlphaNumeric("test1234"));
        }

        [Fact(DisplayName = "Проверка метода " + nameof(Validation.ContainsDigits))]
        public void ContainsDigitsTest()
        {
            Assert.True(Validation.ContainsDigits("Test12344"));
            Assert.False(Validation.ContainsDigits("assadsdasd$#!"));
        }

        [Fact(DisplayName = "Проверка метода " + nameof(Validation.NotContainsCyrillic))]
        public void NotContainsCyrillicTest()
        {
            Assert.True(Validation.NotContainsCyrillic("dsadasdasd"));
            Assert.False(Validation.NotContainsCyrillic("атататdasds"));
        }
    }
}
