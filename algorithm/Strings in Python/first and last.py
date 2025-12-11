s = input()
first = s.find('f')
last = s.rfind('f')
if first != -1:
    if first == last:
        print(first)
    else:
        print(first, last)