s = input()
first = s.find('f')
if first == -1:
    print(-2)
else:
    second = s.find('f', first + 1)
    print(second)