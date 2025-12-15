from dataclasses import dataclass, field


ERR_LENGTH = "length"
ERR_LETTER = "requires_letter"
ERR_DIGIT = "requires_digit"
ERR_SPECIAL = "requires_special"


@dataclass
class PasswordValidationResult:
    is_valid: bool
    errors: list[str] = field(default_factory=list)
    warnings: list[str] = field(default_factory=list)

    def __bool__(self) -> bool:
        return self.is_valid


def validate_password(password: str) -> PasswordValidationResult:
    """
    Проверяет:
    - Минимальная длина >= 12 символов
    - Хотя бы одна буква (a-zA-Z)
    - Хотя бы одна цифра (0-9)
    - Хотя бы один спецсимвол
    """
    errors = []

    if len(password) < 12:
        errors.append(ERR_LENGTH)

    if not any(c.isalpha() for c in password):
        errors.append(ERR_LETTER)

    if not any(c.isdigit() for c in password):
        errors.append(ERR_DIGIT)

    if not any(not c.isalnum() for c in password):
        errors.append(ERR_SPECIAL)

    is_valid = len(errors) == 0
    return PasswordValidationResult(is_valid=is_valid, errors=errors)