s = input()
first = s.find('h')
last = s.rfind('h')
print(s[:first + 1] + s[first + 1:last].replace('h', 'H') + s[last:])