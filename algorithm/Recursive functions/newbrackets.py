def mirror_string(s):
    if len(s) == 0:
        return ""
    
    if s[0] == '(':
        return s[0] + mirror_string(s[1:]) + ')'
    else:
        return s[0] + mirror_string(s[1:]) + s[0]

s = input()
print(mirror_string(s))