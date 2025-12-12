def next_anagram(s):
    if not s or len(s) == 1:
        return s
    
    s = list(s)
    n = len(s)
    
    i = n - 2
    while i >= 0 and s[i] >= s[i + 1]:
        i -= 1
    
    if i == -1:
        s.sort()
        return ''.join(s)
    
    j = n - 1
    while s[j] <= s[i]:
        j -= 1
    
    s[i], s[j] = s[j], s[i]
    
    s[i + 1:] = sorted(s[i + 1:])
    
    return ''.join(s)

try:
    while True:
        word = input().strip()
        if word:
            print(next_anagram(word))
except EOFError:
    pass