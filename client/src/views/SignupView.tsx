import Alert from "react-bootstrap/Alert";
import Badge from "react-bootstrap/Badge";
import Button from "react-bootstrap/Button";
import Card from "@material-ui/core/Card";
import Form from "react-bootstrap/Form";
import { makeStyles } from "@material-ui/core/styles";
import React, { useState } from "react";
import TextField from "@material-ui/core/TextField";

const useStyles = makeStyles((theme) => ({
  alert: {
    display: "inline-block",
    marginTop: -20,
    marginBottom: 40,
    maxWidth: 380,
  },
  badge: {
    marginBottom: 30,
    marginTop: 30,
    width: "45%",
  },
  button: {
    marginTop: 45,
    width: "30%",
  },
  card: {
    marginTop: 70,
    height: 650,
    width: 550,
    borderRadius: 15,
    boxShadow: "0px 0px 0px 8px rgba(12, 12, 242, 0.1)",
    textAlign: "center",
    display: "inline-block",
  },
  helperText: {
    color: "red",
  },
  textField: {
    width: "35.5%",
  },
}));

export const SignupView = () => {
  const styles = useStyles();

  const [email, setEmail] = useState("");
  const [emailError, setEmailError] = useState("");

  const [name, setName] = useState("");
  const [nameError, setNameError] = useState("");

  const [city, setCity] = useState("");
  const [cityError, setCityError] = useState("");

  const [password, setPassword] = useState("");
  const [passwordError, setPassWordError] = useState("");

  const [errorMsg, setErrorMsg] = useState("");

  const handleSubmit = async (event: any) => {
    try {
      event.preventDefault();

      const user = {
        email,
        name,
        city,
        password,
      };

      await signup(user);

      userContext.setUser(user);
    } catch (err) {
      const errors = err.getErrors();

      setEmailError(errors.get("email") || "");
      setNameError(errors.get("name") || "");
      setCityError(errors.get("city") || "");
      setPassWordError(errors.get("password") || "");

      setErrorMsg("An Error Occured");
    }
  };
  return (
    <Card className={styles.card}>
      <h1>
        <Badge className={styles.badge} variant="primary">
          Signup
        </Badge>
      </h1>

      <Form onSubmit={handleSubmit}>
        {errorMsg && (
          <Alert className={styles.alert} variant="danger">
            {errorMsg}
          </Alert>
        )}
        <Form.Group>
          <TextField
            className={styles.textField}
            label="Email"
            onChange={(e) => setEmail(e.target.value)}
            value={email}
            helperText={emailError}
            FormHelperTextProps={{ className: styles.helperText }}
          />
        </Form.Group>

        <Form.Group>
          <TextField
            className={styles.textField}
            label="Name"
            onChange={(e) => setName(e.target.value)}
            value={name}
            helperText={nameError}
            FormHelperTextProps={{ className: styles.helperText }}
          />
        </Form.Group>

        <Form.Group>
          <TextField
            className={styles.textField}
            label="City"
            onChange={(e) => setCity(e.target.value)}
            value={city}
            helperText={cityError}
            FormHelperTextProps={{ className: styles.helperText }}
          />
        </Form.Group>

        <Form.Group>
          <TextField
            className={styles.textField}
            label="Password"
            type="password"
            onChange={(e) => setPassword(e.target.value)}
            value={password}
            helperText={passwordError}
            FormHelperTextProps={{ className: styles.helperText }}
          />
        </Form.Group>

        <Button
          className={styles.button}
          variant="primary"
          type="submit"
          disabled={!(email && name && city && password)}
        >
          Go!
        </Button>
      </Form>
    </Card>
  );
};