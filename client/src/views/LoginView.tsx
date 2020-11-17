import Alert from "react-bootstrap/Alert";
import Badge from "react-bootstrap/Badge";
import Button from "react-bootstrap/Button";
import Card from "@material-ui/core/Card";
import Col from "react-bootstrap/Col";
import Container from "react-bootstrap/Container";
import Form from "react-bootstrap/Form";
import { makeStyles } from "@material-ui/core/styles";
import React, { useState } from "react";
import TextField from "@material-ui/core/TextField";

import { SignupView } from "./SignupView";

const useStyles = makeStyles((theme) => ({
  alert: {
    display: "inline-block",
    marginTop: -20,
    marginBottom: 40,
    maxWidth: 380,
  },
  badge: {
    marginBottom: 40,
    marginTop: 25,
    width: "45%",
  },
  button: {
    marginTop: 25,
    marginBottom: -15,
    width: "30%",
  },
  buttonGroup: {
    marginTop: 35,
    marginBottom: -15,
    width: "100%",
  },
  card: {
    marginTop: 70,
    height: 475,
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
    width: "35%",
  },
  wrapper: {
    textAlign: "center",
  },
}));

export const LoginView = () => {
  const styles = useStyles();

  const [renderSignUp, setRenderSignUp] = useState(false);

  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [errorMsg, setErrorMsg] = useState("");

  const handleSubmit = async (event: any) => {
    try {
      event.preventDefault();

      const loginRequest = {
        email,
        password,
      };

      //const user = await login(loginRequest);

    } catch (err) {
      setErrorMsg("Wrong email or password");
    }
  };

  return (
    <Container fluid>
      <Col className={styles.wrapper}>
        {renderSignUp ? (
          <SignupView />
        ) : (
          <Card className={styles.card}>
            <h1>
              <Badge className={styles.badge} variant="primary">
                Login
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
                />
              </Form.Group>

              <Form.Group>
                <TextField
                  className={styles.textField}
                  label="Password"
                  type="password"
                  onChange={(e) => setPassword(e.target.value)}
                  value={password}
                />
              </Form.Group>
              <div className={styles.buttonGroup}>
                <Button
                  className={styles.button}
                  variant="primary"
                  type="submit"
                  disabled={!(email && password)}
                >
                  Login
                </Button>
                <br />
                <Button
                  onClick={() => setRenderSignUp(true)}
                  className={styles.button}
                  variant="secondary"
                >
                  Signup
                </Button>
              </div>
            </Form>
          </Card>
        )}
      </Col>
    </Container>
  );
};