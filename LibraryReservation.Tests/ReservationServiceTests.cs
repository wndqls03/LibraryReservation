using Microsoft.VisualStudio.TestTools.UnitTesting;
using LibraryReservation;

namespace LibraryReservation.Tests
{
    [TestClass]
    public class ReservationServiceTests
    {
        // REQ-LIB-01 / AC-01: available book + valid member -> reservation succeeds
        [TestMethod]
        public void ReserveBook_AvailableBookAndValidMember_ReturnsSuccess()
        {
            var book = new Book("B001", "Software Testing Basics");
            var member = new Member("M001", "Aroha Smith");
            var service = new ReservationService();

            ReservationResult result = service.ReserveBook(book, member);

            Assert.IsTrue(result.Success);
            StringAssert.Contains(result.Message, "Reservation successful");
        }

        // REQ-LIB-01: successful reservation marks the book as reserved
        [TestMethod]
        public void ReserveBook_AvailableBook_MarksBookAsReserved()
        {
            var book = new Book("B001", "Software Testing Basics");
            var member = new Member("M001", "Aroha Smith");
            var service = new ReservationService();

            service.ReserveBook(book, member);

            Assert.IsTrue(book.IsReserved);
        }

        // REQ-LIB-02: empty member ID is rejected at construction time
        [TestMethod]
        public void Member_EmptyMemberId_ThrowsException()
        {
            Assert.ThrowsExactly<ArgumentException>(() =>
                new Member("", "Aroha Smith"));
        }

        // REQ-LIB-03 / AC-03: a book that is already reserved cannot be reserved again
        [TestMethod]
        public void ReserveBook_AlreadyReservedBook_ReturnsFailure()
        {
            var book = new Book("B001", "Software Testing Basics");
            var member1 = new Member("M001", "Aroha Smith");
            var member2 = new Member("M002", "John Chen");
            var service = new ReservationService();

            service.ReserveBook(book, member1);
            ReservationResult result = service.ReserveBook(book, member2);

            Assert.IsFalse(result.Success);
            StringAssert.Contains(result.Message, "already reserved");
        }

        // REQ-LIB-04 / AC-04: a null book produces a clear failure message
        [TestMethod]
        public void ReserveBook_NullBook_ReturnsClearFailureMessage()
        {
            var member = new Member("M001", "Aroha Smith");
            var service = new ReservationService();

            ReservationResult result = service.ReserveBook(null, member);

            Assert.IsFalse(result.Success);
            StringAssert.Contains(result.Message, "book details are required");
        }

        // REQ-LIB-04 / AC-04: a null member produces a clear failure message
        [TestMethod]
        public void ReserveBook_NullMember_ReturnsClearFailureMessage()
        {
            var book = new Book("B001", "Software Testing Basics");
            var service = new ReservationService();

            ReservationResult result = service.ReserveBook(book, null);

            Assert.IsFalse(result.Success);
            StringAssert.Contains(result.Message, "member details are required");
        }
    }
}
