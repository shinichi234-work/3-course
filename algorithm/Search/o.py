n = int(input())
children = {}

for _ in range(n):
    msg_id, parent_id = map(int, input().split())
    if parent_id not in children:
        children[parent_id] = []
    children[parent_id].append(msg_id)

k = int(input())

def count_deleted(msg_id):
    count = 1
    if msg_id in children:
        for child in children[msg_id]:
            count += count_deleted(child)
    return count

print(count_deleted(k))