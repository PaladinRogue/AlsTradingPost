gcloud kms decrypt --plaintext-file appSecrets.json --ciphertext-file appSecrets.json.encrypted --key (Get-Content appSecrets.json.keyname) 
