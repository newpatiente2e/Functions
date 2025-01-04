import { useEffect, useState } from "react";
import { Link, useLoaderData } from "react-router-dom";
import { PatientList } from "../components/PatientList";
import { Patient } from "../models/Patient";

const Doctor = () => {
  const [patients, setPatients] = useState<Patient[]>([]);

  useEffect(() => {
    fetch("/api/patient")
      .then((response) => response.json())
      .then((data) => setPatients(data));
  }, []);

  const action = (patient: Patient) => {
    return <Link to={`/patient/${patient.id}`}>View patient</Link>;
  };

  return <PatientList patients={patients || []} action={action} />;
};

export default Doctor;
