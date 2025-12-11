n = int(input())
s = set()

for _ in range(n):
    query = input().split()
    
    if query[0] == 'ADD':
        num = int(query[1])
        s.add(num)
    elif query[0] == 'PRESENT':
        num = int(query[1])
        if num in s:
            print('YES')
        else:
            print('NO')
    elif query[0] == 'COUNT':
        print(len(s))