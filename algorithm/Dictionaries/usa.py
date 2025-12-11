votes = {}

try:
    while True:
        line = input().strip()
        if not line:
            continue
        
        parts = line.split()
        candidate = parts[0]
        count = int(parts[1])
        
        if candidate in votes:
            votes[candidate] += count
        else:
            votes[candidate] = count
except EOFError:
    pass

for candidate in sorted(votes.keys()):
    print(candidate, votes[candidate])