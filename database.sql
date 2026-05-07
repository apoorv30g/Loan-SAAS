CREATE TABLE clients (
  id SERIAL PRIMARY KEY,
  name TEXT NOT NULL,
  email TEXT UNIQUE NOT NULL,
  password_hash TEXT NOT NULL,
  created_at TIMESTAMP DEFAULT NOW()
);

CREATE TABLE leads (
  id SERIAL PRIMARY KEY,
  client_id INT NOT NULL REFERENCES clients(id),

  name TEXT,
  phone TEXT,

  loan_amount INT,
  drop_stage TEXT,

  score INT DEFAULT 0,
  status TEXT DEFAULT 'pending',
  attempts INT DEFAULT 0,

  last_call_outcome TEXT,

  created_at TIMESTAMP DEFAULT NOW()
);

CREATE INDEX idx_leads_client ON leads(client_id);
CREATE INDEX idx_leads_status ON leads(status);

CREATE TABLE client_costs (
  id SERIAL PRIMARY KEY,

  client_id INT REFERENCES clients(id),
  lead_id INT REFERENCES leads(id),

  type TEXT, -- call / sms / ai
  provider TEXT, -- twilio / elevenlabs

  cost DECIMAL NOT NULL,

  created_at TIMESTAMP DEFAULT NOW()
);

CREATE INDEX idx_cost_client ON client_costs(client_id);

CREATE TABLE call_logs (
  id SERIAL PRIMARY KEY,
  client_id INT REFERENCES clients(id),
  lead_id INT REFERENCES leads(id),

  provider TEXT, -- twilio / elevenlabs
  call_status TEXT, -- initiated / answered / failed
  outcome TEXT, -- interested / not_interested / callback / no_answer

  duration_seconds INT,
  cost DECIMAL,

  created_at TIMESTAMP DEFAULT NOW()
);

CREATE INDEX idx_call_logs_lead ON call_logs(lead_id);
ALTER TABLE leads ADD COLUMN called BOOLEAN DEFAULT FALSE;