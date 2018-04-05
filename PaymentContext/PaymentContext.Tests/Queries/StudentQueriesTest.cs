using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Queries;
using System.Collections.Generic;
using System.Linq;

namespace PaymentContext.Tests.Queries
{
    [TestClass]
    public sealed class StudentQueriesTest : TestBase
    {
        private readonly IList<Student> _fakeStudents;

        public StudentQueriesTest()
        {
            _fakeStudents = MontarUmaListaCom30Estudantes();
        }

        [TestMethod]
        public void DeveRetornarNullQuandoDocumentoNaoExiste()
        {           
            var expression = StudentQueries.GetStudent("03489780302");
            var student = _fakeStudents.AsQueryable()
                .Where(expression)
                .FirstOrDefault();

            student.Should().BeNull();
        }

        [TestMethod]
        public void DeveRetornarEstudante()
        {
            var cpf = _fakeStudents[20].Document.Number;
            var expression = StudentQueries.GetStudent(cpf);
            var student = _fakeStudents.AsQueryable()
                .Where(expression)
                .FirstOrDefault();

            student.Should().NotBeNull();
        }

    }
}
