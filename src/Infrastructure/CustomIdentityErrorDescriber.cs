using Microsoft.AspNetCore.Identity;

namespace Infrastructure;

public class CustomIdentityErrorDescriber : IdentityErrorDescriber
{
    public override IdentityError DefaultError() { return new IdentityError { Code = nameof(DefaultError), Description = $"��������� ����������� ������" }; }
    public override IdentityError ConcurrencyFailure() { return new IdentityError { Code = nameof(ConcurrencyFailure), Description = "������ �������������� �������� ������������, ������ ��� ������" }; }
    public override IdentityError PasswordMismatch() { return new IdentityError { Code = nameof(PasswordMismatch), Description = "����������� ������" }; }
    public override IdentityError InvalidToken() { return new IdentityError { Code = nameof(InvalidToken), Description = "���������������� �����" }; }
    public override IdentityError LoginAlreadyAssociated() { return new IdentityError { Code = nameof(LoginAlreadyAssociated), Description = "������������ � ����� ������� ��� ����������" }; }
    public override IdentityError InvalidUserName(string userName) { return new IdentityError { Code = nameof(InvalidUserName), Description = $"��� ������������ '{userName}' �����������, ����� ��������� ������ ����� � �����" }; }
    public override IdentityError InvalidEmail(string email) { return new IdentityError { Code = nameof(InvalidEmail), Description = $"Email '{email}' �����������" }; }
    public override IdentityError DuplicateUserName(string userName) { return new IdentityError { Code = nameof(DuplicateUserName), Description = $"������������ � ������ '{userName}' ��� ����������" }; }
    public override IdentityError DuplicateEmail(string email) { return new IdentityError { Code = nameof(DuplicateEmail), Description = $"Email '{email}' ��� ������������" }; }
    public override IdentityError InvalidRoleName(string role) { return new IdentityError { Code = nameof(InvalidRoleName), Description = $"��� ���� '{role}' �����������" }; }
    public override IdentityError DuplicateRoleName(string role) { return new IdentityError { Code = nameof(DuplicateRoleName), Description = $"��� ���� '{role}' ��� ������������" }; }
    public override IdentityError UserAlreadyHasPassword() { return new IdentityError { Code = nameof(UserAlreadyHasPassword), Description = "������������ ��� ��������� ������" }; }
    public override IdentityError UserLockoutNotEnabled() { return new IdentityError { Code = nameof(UserLockoutNotEnabled), Description = "���������� ���������� ��� ����� ������������" }; }
    public override IdentityError UserAlreadyInRole(string role) { return new IdentityError { Code = nameof(UserAlreadyInRole), Description = $"������������ ��� ��������� ���� '{role}'." }; }
    public override IdentityError UserNotInRole(string role) { return new IdentityError { Code = nameof(UserNotInRole), Description = $"� ������������ ��� ���� '{role}'." }; }
    public override IdentityError PasswordTooShort(int length) { return new IdentityError { Code = nameof(PasswordTooShort), Description = $"������ ������ ���� ������ �� ����� {length} ��������" }; }
    public override IdentityError PasswordRequiresNonAlphanumeric() { return new IdentityError { Code = nameof(PasswordRequiresNonAlphanumeric), Description = "������ ������ ���� ��������� ���� �� ���� �� ��������-�������� ������" }; }
    public override IdentityError PasswordRequiresDigit() { return new IdentityError { Code = nameof(PasswordRequiresDigit), Description = "������ ������ ��������� ���� �� ���� ����� ('0'-'9')." }; }
    public override IdentityError PasswordRequiresLower() { return new IdentityError { Code = nameof(PasswordRequiresLower), Description = "������ ������ ��������� ���� �� ���� ������ � ������ �������� ('a'-'z')." }; }
    public override IdentityError PasswordRequiresUpper() { return new IdentityError { Code = nameof(PasswordRequiresUpper), Description = "������ ������ ��������� ���� �� ���� ������ � ������� �������� ('A'-'Z')" }; }
}