def add_stars(s):
    if len(s) <= 1:
        return s
    
    return s[0] + '*' + add_stars(s[1:])

s = input()
print(add_stars(s))