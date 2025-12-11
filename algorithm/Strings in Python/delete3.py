s = input()
result = ''
for i in range(len(s)):
    if i % 3 != 0:
        result += s[i]
print(result)