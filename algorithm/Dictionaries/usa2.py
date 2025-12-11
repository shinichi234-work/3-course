n = int(input())
states = {}

for _ in range(n):
    line = input().split()
    state_name = line[0]
    electors = int(line[1])
    states[state_name] = {'electors': electors, 'votes': {}}

try:
    while True:
        line = input().split()
        state_name = line[0]
        candidate = line[1]
        
        if candidate not in states[state_name]['votes']:
            states[state_name]['votes'][candidate] = 0
        states[state_name]['votes'][candidate] += 1
except EOFError:
    pass

candidate_electors = {}

for state_name, state_data in states.items():
    if state_data['votes']:
        max_votes = max(state_data['votes'].values())
        winners = [c for c, v in state_data['votes'].items() if v == max_votes]
        winner = sorted(winners)[0]
        
        if winner not in candidate_electors:
            candidate_electors[winner] = 0
        candidate_electors[winner] += state_data['electors']

all_candidates = set()
for state_data in states.values():
    all_candidates.update(state_data['votes'].keys())

for candidate in all_candidates:
    if candidate not in candidate_electors:
        candidate_electors[candidate] = 0

result = []
for candidate, electors in candidate_electors.items():
    result.append((-electors, candidate))

result.sort()

for neg_electors, candidate in result:
    print(f"{candidate} {-neg_electors}")