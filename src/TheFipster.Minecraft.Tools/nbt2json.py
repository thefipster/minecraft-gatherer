import sys
import nbtlib
import json
import numpy as np

class NumpyEncoder(json.JSONEncoder):
    def default(self, obj):
        if isinstance(obj, np.ndarray):
            return obj.tolist()
        return json.JSONEncoder.default(self, obj)

nbt_file = nbtlib.load(sys.argv[1])
dump = json.dumps(nbt_file, cls=NumpyEncoder)
print(dump)
