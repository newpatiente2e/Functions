import { useLoaderData } from "react-router-dom";
import { Patient } from "../models/Patient";
import { listing, listingItemLabel } from "./PatientDetails.css";

const PatientDetails = () => {
  const patient = useLoaderData() as Patient;

  return (
    <>
      <h1>Patient Details</h1>
      <div>
        <h2>
          {patient.givenNames} {patient.familyName}
        </h2>
        <ul className={listing}>
          <li>
            <span className={listingItemLabel}>Date of birth:</span>{" "}
            {patient.dateOfBirth}
          </li>
          <li>
            <span className={listingItemLabel}>Phone:</span> {patient.phone}
          </li>
          <li>
            <span className={listingItemLabel}>Email:</span> {patient.email}
          </li>
          <li>
            <span className={listingItemLabel}>Gender:</span> {patient.gender}
          </li>
          <li>
            <span className={listingItemLabel}>Pronouns:</span>{" "}
            {patient.pronouns}
          </li>
          <li>
            <span className={listingItemLabel}>Address:</span>{" "}
            {patient.addressStreet} {patient.addressCity} {patient.addressState}{" "}
            {patient.addressPostcode}
          </li>
          <li>
            <span className={listingItemLabel}>Emergency contact:</span>{" "}
            {patient.emergencyName} ({patient.emergencyRelationship}){" "}
            {patient.emergencyPhone} {patient.emergencyEmail}
          </li>
          <li>
            <span className={listingItemLabel}>Insurance Number:</span>{" "}
            {patient.insuranceNumber}
          </li>
          <li>
            <table>
              <thead>
                <tr>
                  <th>Medication</th>
                  <th>Reaction</th>
                </tr>
              </thead>
              <tbody>
                {patient.allergies.map((allergy) => (
                  <tr>
                    <td>{allergy.medication}</td>
                    <td>{allergy.reaction}</td>
                  </tr>
                ))}
              </tbody>
            </table>
          </li>
          <li>
            <span className={listingItemLabel}>Preferred contact options:</span>{" "}
            {patient.preferEmail ? "Email" : ""}{" "}
            {patient.preferPhone ? "Phone" : ""}{" "}
            {patient.preferText ? "Text" : ""}
          </li>
        </ul>
      </div>
    </>
  );
};

export default PatientDetails;
