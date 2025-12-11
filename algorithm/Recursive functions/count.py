def count_digits(s):
    if len(s) == 0:
        return 0
    
    count = 1 if s[0].isdigit() else 0
    return count + count_digits(s[1:])

s = input()
print(count_digits(s))