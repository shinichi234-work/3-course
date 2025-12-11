n = int(input())
participants = []
for _ in range(n):
    line = input().split()
    name = line[0]
    score = int(line[1])
    participants.append((score, name))

participants.sort(reverse=True)

for score, name in participants:
    print(name)