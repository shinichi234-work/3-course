def max_digit(s):
    if len(s) == 1:
        return s[0]
    
    max_rest = max_digit(s[1:])
    
    if s[0] > max_rest:
        return s[0]
    else:
        return max_rest

s = input()
print(max_digit(s))