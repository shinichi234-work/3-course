n = int(input())
yes_sets = []
no_set = set()

while True:
    line = input()
    if line == 'HELP':
        break
    
    numbers = set(map(int, line.split()))
    answer = input()
    
    if answer == 'YES':
        yes_sets.append(numbers)
    else:
        no_set.update(numbers)

if yes_sets:
    possible = yes_sets[0].copy()
    for s in yes_sets[1:]:
        possible &= s
    possible -= no_set
else:
    possible = set(range(1, n + 1)) - no_set

print(*sorted(possible))