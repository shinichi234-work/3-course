n = int(input())
possible = set(range(1, n + 1))

while True:
    line = input()
    if line == 'HELP':
        break
    
    numbers = set(map(int, line.split()))
    
    in_possible = numbers & possible
    not_in_possible = possible - numbers
    
    if len(not_in_possible) >= len(in_possible):
        print('NO')
        possible = not_in_possible
    else:
        print('YES')
        possible = in_possible

print(*sorted(possible))