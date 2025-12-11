n = int(input())
participants = []
for _ in range(n):
    id_num, score = map(int, input().split())
    participants.append((score, id_num))

participants.sort(key=lambda x: (-x[0], x[1]))

for score, id_num in participants:
    print(id_num, score)