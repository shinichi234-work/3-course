num = input()
digits = sorted(num)

if digits[0] == '0':
    for i in range(1, 4):
        if digits[i] != '0':
            digits[0], digits[i] = digits[i], digits[0]
            break

print(''.join(digits))