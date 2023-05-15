# Normalize EOIR Images

## [create_normalized_frame_sequence.m](create_normalized_frame_sequence.m)

This script takes a folder of eoir raw data files and normalizes the collection. That's because each frame of an EOIR synthetic scene is normalized to itself, so there's color variation in some frames. This script normalizes the collection to the first (or the selected frame). 

### Dependencies

* Capabilities: N/A
* Other Scripts: [save_colormapped_image.m](save_colormapped_image.m)
* Scenario: N/A

---

## [save_colormapped_image.m](save_colormapped_image.m)

This script is run from the create_normalized_frame_sequence.m script. It takes the normalized data and saves it as a colormapped image. 

### Dependencies

* Capabilities: N/A
* Other Scripts: [create_normalized_frame_sequence.m](create_normalized_frame_sequence.m)
* Scenario: N/A