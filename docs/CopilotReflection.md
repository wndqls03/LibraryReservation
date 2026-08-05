# Copilot Reflection — Step 7

## Prompt Used
Suggest MSTest unit tests for this C# library reservation system. Focus on 
testable requirements, acceptance criteria, edge cases, and clear expected results.

## One Useful Suggestion
**CreateReservation_ResourceMarkedUnavailable_Fails**

This mapped well onto REQ-LIB-03 (reject a reservation if the book is already 
reserved). I adapted it to the actual API and renamed it 
`ReserveBook_AlreadyReservedBook_ReturnsFailure` to match the domain 
vocabulary (Book, not "resource").

## One Suggestion Modified
**Original:** `CreateReservation_NullOrInvalidUserId_ValidationFails`
Expected an `ArgumentNullException` to be thrown.

**Problem:** `ReservationService.ReserveBook` doesn't throw on invalid 
input — it returns a `ReservationResult(false, message)` per REQ-LIB-04 
("clear success or failure message for every attempt").

**Modified to:** `ReserveBook_NullMember_ReturnsClearFailureMessage`, 
asserting `Assert.IsFalse(result.Success)` and 
`StringAssert.Contains(result.Message, "member details are required")` 
instead of expecting a thrown exception.

## One Suggestion Rejected
Rejected all concurrency and time/capacity-related tests:
- `CreateReservation_ConcurrentRequests_SameSlot_OnlyOneSucceeds`
- `UpdateReservation_ConcurrentModification_DetectsConflict`
- `CreateReservation_StartInPast_ValidationFails`
- `CreateReservation_ExceedsMaximumDuration_ValidationFails`
- `CreateReservation_PersistsAuditFieldsAndTimestamps`

**Reason:** These assume features that don't exist in this system — time 
ranges, a database layer, and concurrent access control. None of 
REQ-LIB-01 through REQ-LIB-04 mention scheduling, persistence, or 
concurrency, so these tests would validate behaviour outside the defined 
scope.

## Why Human Judgement Was Required
Copilot pattern-matched "library reservation system" to a generic 
resource-booking system (like meeting rooms or hotel rooms) from its 
training data, rather than reading the actual `Book`, `Member`, and 
`ReservationService` classes or the REQ-LIB requirements. Over half of 
its suggestions tested functionality that doesn't exist in this codebase. 
Deciding which suggestions actually corresponded to REQ-LIB-01–04 required 
comparing each one against the requirements document and the real service 
API — something Copilot has no access to on its own.