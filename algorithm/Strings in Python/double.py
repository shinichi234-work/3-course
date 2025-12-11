s = input()
first = s.find('h')
last = s.rfind('h')
middle = s[first + 1:last]
print(s[:first + 1] + middle + middle + s[last:])