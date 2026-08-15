# Requirements Traceability Matrix — Library Book Reservation System

| Requirement ID | Requirement Summary | Acceptance Criteria | Test Case | Status |
|---|---|---|---|---|
| REQ-LIB-01 | Reserve only available book | AC-01 | ReserveBook_AvailableBookAndValidMember_ReturnsSuccess; ReserveBook_AvailableBook_MarksBookAsReserved | Passed |
| REQ-LIB-02 | Reject empty member ID | AC-02 | Member_EmptyMemberId_ThrowsException | Passed |
| REQ-LIB-03 | Reject already reserved book | AC-03 | ReserveBook_AlreadyReservedBook_ReturnsFailure | Passed |
| REQ-LIB-04 | Return clear success or failure message | AC-04 | ReserveBook_NullBook_ReturnsClearFailureMessage; ReserveBook_NullMember_ReturnsClearFailureMessage | Passed |

Traceability helps the team check whether each requirement has test evidence. It also
supports change management because if a requirement changes, the related test cases can
be identified, reviewed, and updated.

## Managing Requirement Change — REQ-LIB-05

The library adds a new rule: **REQ-LIB-05 — A member cannot reserve more than one book at
the same time.**

**Which class may need to change?**
`ReservationService` would need to change most, since it currently has no awareness of a
member's existing reservations. It would need either a reference to a repository/collection
of active reservations per member, or the `Member` class would need to track its own
current reservation(s) (e.g. a `HasActiveReservation` flag or list). `ReserveBook` would then
need an extra check before allowing a new reservation to proceed.

**Which test cases need to be added?**
- `ReserveBook_MemberAlreadyHasActiveReservation_ReturnsFailure`
- `ReserveBook_MemberWithNoActiveReservation_ReturnsSuccess`
- `ReserveBook_MemberReservationReleased_CanReserveAgain` (if a release/return flow exists)

**What should be added to the RTM?**
A new row for REQ-LIB-05, linked to a new acceptance criterion (e.g. AC-05: *Given a member
who already has an active reservation, when they attempt to reserve another book, then the
reservation fails*), and the new test case names above, with a status once implemented and
passing.
